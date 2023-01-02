package com.backend.app.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.util.StringUtils;
import org.springframework.web.multipart.MultipartFile;

import com.backend.app.model.ImageFileData;
import com.backend.app.repository.ImageFileDataRepo;

import java.io.IOException;
import java.time.LocalDateTime;
import java.time.format.DateTimeFormatter;
import java.util.List;
import java.util.NoSuchElementException;
import java.util.Optional;

import java.io.File;
import java.nio.file.Files;

@Service
public class ImageFileDataService {

    @Autowired
    private ImageFileDataRepo repository;

    @Autowired
    private AppUserService userService;

    private final String FOLDER_PATH = "/home/ubuntu/MyAndroidApp/backend/pictures/";

    public String uploadImageToFileSystem(MultipartFile image, Long ownerId) {

        // check if owner id exist
        if(userService.getUserId(ownerId).isPresent()) {

            // only upload png, jpeg, and gif
            if(image.getContentType().equals("image/jpeg") || image.getContentType().equals("image/png") || image.getContentType().equals("image/gif")) {
                        
                // format time
                DateTimeFormatter timeFormat = DateTimeFormatter.ofPattern("yyyyMMddHHmmssSSS");
                String presentTime = LocalDateTime.now().format(timeFormat);

                // get file extension
                String imageFileExtension = StringUtils.getFilenameExtension(image.getOriginalFilename()).toLowerCase();

                // create new image file name concat the present time and file extension
                String imageFileName = presentTime + "." + imageFileExtension;

                // make file path location
                String filePath = FOLDER_PATH.concat(imageFileName);

                // save image file data to database
                ImageFileData imageFileData = repository.save(ImageFileData.builder()
                        .name(imageFileName)
                        .type(image.getContentType())
                        .filePath(filePath)
                        .userOwner(ownerId) // owner id should be the id of the user account
                        .viewImage("http://13.215.47.77:8080/api/image/view?imageName=".concat(imageFileName))
                        .build());

                // if image path is null then its not successful
                if (imageFileData.equals(null)) {

                    throw new IllegalStateException("Image file data is empty, cannot upload the image");

                }

                try {

                    // transfer image to file path location folder
                    image.transferTo(new File(filePath));

                } catch (IOException ex) {

                    throw new IllegalStateException("Cannot upload file from the file system.");

                }

                return "Image uploaded successfully : ".concat(filePath);

            }

            // attempt to upload non-image file
            throw new IllegalArgumentException("Invalid file, not an image");

        }

        throw new IllegalArgumentException("Owner id does not exist");

    }

    public byte[] loadImageFromFileSystem(String imageName) {

        try {

        // find the image file data from database using the image name
        Optional<ImageFileData> imageFileData = repository.findByName(imageName);

        // get image path location
        String filePath = imageFileData.get().getFilePath();

            // read image byte code
            byte[] image = Files.readAllBytes(new File(filePath).toPath());
            
            // return image byte code
            return image;

        } catch(IOException | NoSuchElementException ex) {

            throw new IllegalStateException("Image does not exist, cannot view image.");

        }

    }

    public String deleteImageFromFileSystem(String imageName) {

        try {

            // find the image file data from database using the image name
            Optional<ImageFileData> imageFileData = repository.findByName(imageName);

            // get image path location
            String filePath = imageFileData.get().getFilePath();

            //get image id
            Long imageId = imageFileData.get().getId();

            boolean isImageDeleted = Files.deleteIfExists(new File(filePath).toPath());

            // is image has been deleted from the file system
            if(isImageDeleted) {

                // delete image file data from the database
                repository.deleteById(imageId);
                
                return "Image has been successfully deleted";
            }

            throw new IllegalStateException("Image has been unsuccessfully deleted");

        } catch(IOException | NoSuchElementException ex) {

            throw new IllegalStateException("Image does not exist, cannot delete image");

        }
        
    }

    public List<ImageFileData> AllOfImageFileData() {

        // pass a list of image file data 
        return repository.findAll();

    }

    public Optional<List<ImageFileData>> listOfImageFileData(Long ownerId) { // param is for testing only

        // pass a list of image file data sort by owner id 
        return repository.findByUserOwner(ownerId);
        
    }

}
