package com.backend.app.controller;

import java.util.Optional;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.HttpStatus;
import org.springframework.http.MediaType;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.PutMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import com.backend.app.service.AppUserService;
import com.backend.app.model.AppUser;
import com.backend.app.model.AppUserLogin;
import com.backend.app.model.AppUserResetPass;

@RestController
@RequestMapping("/api/users")
public class AppUserController {

    @Autowired
    private AppUserService service;

    @PostMapping("/login")
    public ResponseEntity<?> login(@RequestBody AppUserLogin appUserLogin) {

        Optional<AppUser> userLogin = service.userLogin(appUserLogin);

        return ResponseEntity.status(HttpStatus.OK)
                .contentType(MediaType.valueOf("application/json"))
                .body(userLogin);

    }

    @PostMapping("/register")
    public ResponseEntity<?> register(@RequestBody AppUser userInfo) {

        String register = service.userRegister(userInfo);

        return ResponseEntity.status(HttpStatus.CREATED)
                .body(register);
    }

    @PutMapping("/reset/password")
    public ResponseEntity<?> resetPassword(@RequestBody AppUserResetPass userResetPassInfo) {

        String passwordReset = service.resetPassword(userResetPassInfo);

        return ResponseEntity.status(HttpStatus.OK)
                .body(passwordReset);

    }

}
