create database student_portal;

CREATE TABLE Student (
    ID int NOT NULL AUTO_INCREMENT,
	StudentId int NOT NULL,
    Name varchar(255) NOT NULL,
    Email varchar(255),
    Phone int,
    Address varchar(255),
    Course varchar(255),
	Gender varchar(255),
    PRIMARY KEY (ID)
);


CREATE TABLE login_users (
    ID int NOT NULL AUTO_INCREMENT,
    username varchar(255) NOT NULL,
    password varchar(255) NOT NULL,
    PRIMARY KEY (ID)
);


insert into login_users (username,password) values('admin','admin'); 

