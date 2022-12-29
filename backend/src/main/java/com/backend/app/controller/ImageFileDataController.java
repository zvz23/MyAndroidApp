package com.backend.app.controller;

import java.io.IOException;
import java.util.List;
import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RequestParam;
import org.springframework.web.bind.annotation.RestController;
import org.springframework.web.multipart.MultipartFile;

import com.backend.app.model.ImageFileData;
import com.backend.app.service.ImageFileDataService;

import org.springframework.web.bind.annotation.DeleteMapping;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PostMapping;

@RestController
@RequestMapping("/api/image")
public class ImageFileDataController {

    @Autowired
    private ImageFileDataService service;

    @PostMapping("/upload")
    public ResponseEntity<?> uploadImage(@RequestParam("image") MultipartFile image, @RequestParam("ownerId") Long ownerId) {

        String upload = service.uploadImageToFileSystem(image, ownerId);

        return ResponseEntity.status(HttpStatus.CREATED)
                .body(upload);

    }

    @GetMapping("/view")
    public ResponseEntity<?> loadImage(@RequestParam("imageName") String imageName) throws IOException {

        // get image type from file extention
        String imageType = imageName.substring(imageName.lastIndexOf("."))
                .substring(1)
                .toLowerCase();

        // check if jpg exist to turn it into jpeg
        if (imageType.equals("jpg")) {
            imageType = "jpeg";
        }

        byte[] imageData = service.loadImageFromFileSystem(imageName);

        return ResponseEntity.status(HttpStatus.OK)
                .contentType(MediaType.valueOf("image/" + imageType))
                .body(imageData);

    }

    @DeleteMapping("/delete")
    public ResponseEntity<?> deleteImage(@RequestParam("imageName") String imageName) {

        String delete = service.deleteImageFromFileSystem(imageName);

        return ResponseEntity.status(HttpStatus.OK)
                .body(delete);
    }

    @GetMapping("/list") // it should be no parameter.
    public ResponseEntity<?> listOfImageData(@RequestParam("ownerId") Long ownerId) {

        Optional<List<ImageFileData>> data = service.listOfImageFileData(ownerId);

        return ResponseEntity.status(HttpStatus.OK)
                .body(data);

    }

}
