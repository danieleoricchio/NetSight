<?php
    require 'classes/laboratorio.php';
    require 'classes/pc.php';
    require 'classes/json_message.php';

    header("Content-type: application/json;charset=utf-8");
    $link= mysqli_connect("localhost","root","","db_netsight");
    if($link === false){
        die();
    }

    if ($_SERVER['REQUEST_METHOD']!="GET") die(json_encode(new JsonMessage(400, "Utilizzare metodo GET", false,null)));
    if (!isset($_GET['email']) || empty($_GET['email'])) die(json_encode(new JsonMessage(400, "Parametro 'email' non inserito", false,null)));
    $email = $_GET['email'];
    $sql = "SELECT Password FROM utenti WHERE utenti.Email = '$email'";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        $Password = $all[0][0];
        echo json_encode(new JsonMessage(200, "Operazione completata", true, $Password));
    }
    mysqli_close($link);