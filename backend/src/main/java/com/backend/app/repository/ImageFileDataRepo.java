package com.backend.app.repository;

import java.util.List;
import java.util.Optional;

import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import com.backend.app.model.ImageFileData;

@Repository
public interface ImageFileDataRepo extends JpaRepository<ImageFileData, Long> {

    Optional<ImageFileData> findByName(String name);

    Optional<List<ImageFileData>> findByUserOwner(Long id);

}
