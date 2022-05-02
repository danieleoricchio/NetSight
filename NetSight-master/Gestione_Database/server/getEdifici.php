<?php
    require 'classes/edificio.php';

    header("Content-type: application/json;charset=utf-8");
    $link= mysqli_connect("localhost","root","","db_netsight");
    if($link === false){
        die();
    }
    if ($_SERVER['REQUEST_METHOD']!="GET") die(json_encode(new JsonMessage(400, "Utilizzare metodo GET", false)));
    $edifici = array();
    $sql = "SELECT * FROM edifici ORDER BY Cod";
    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        foreach ($all as $value) {
            array_push($edifici, new edificio(intval($value[0]),$value[1],$value[2]));
        }
        echo json_encode($edifici);
    }
    mysqli_close($link);
?>