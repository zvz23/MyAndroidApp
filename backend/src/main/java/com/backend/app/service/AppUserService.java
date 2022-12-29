package com.backend.app.service;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.backend.app.repository.AppUserRepo;
import com.backend.app.model.AppUser;
import com.backend.app.model.AppUserLogin;
import com.backend.app.model.AppUserResetPass;

import java.util.List;
import java.util.Optional;

@Transactional
@Service
public class AppUserService {

    @Autowired
    private AppUserRepo repository;

    public List<AppUser> getUsers() {
        return repository.findAll();
    }
    
    public Optional<AppUser> userLogin(AppUserLogin appUserLogin) {

        // get user data from the database
        Optional<AppUser> userData = repository.findByEmail(appUserLogin.getEmail());

        // check if user data is null, if null return exception
        if(!userData.isPresent()) {
            throw new IllegalStateException("Invalid credentials.");
        }

        // check if password are match, if not return exception
        if(!userData.get().getPassword().equals(appUserLogin.getPassword())) {
            throw new IllegalStateException("Invalid credentials.");
        }

        // all credentials are valid return user data
        return userData;

    }

    public String userRegister(AppUser userInfo) {

        // check if email exist, then user data will be present
        Optional<AppUser> userData = repository.findByEmail(userInfo.getEmail());

        // if user data is not present
        if(!userData.isPresent()) {

            // register new user to database
            repository.save(userInfo);
            
            return "User is successfully registered.";

        }

        throw new IllegalStateException("Email is taken.");

    }

    @Transactional
    public String resetPassword(AppUserResetPass userResetPassinfo) {

        // check if email exist, if exist then get that user data
        Optional<AppUser> userData = repository.findByEmail(userResetPassinfo.getEmail());

        // is data is present
        if(userData.isPresent()) {
            
            // set the new password
            userData.get().setPassword(userResetPassinfo.getNewPassword());

            return "Password successfully reset";

        }

        // if data is not present, meaning email does not exist
        throw new IllegalStateException("Email does not exist");

    }

    public Optional<AppUser> getUserId(Long id) {
        return repository.findById(id);
    }

}
