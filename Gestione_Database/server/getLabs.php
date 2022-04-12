<?php
    class laboratorio
    {
        public $nome,$cod,$codEdificio;

        public function __construct($nome,$cod,$codEdificio){
            $this -> $nome = $nome;
            $this -> $cod = $cod;
            $this -> $codEdificio = $codEdificio;
        }
    }
    header("Content-type: application/json;charset=utf-8");
    $link= mysqli_connect("localhost","root","","db_netsight");
    if($link === false){
        die();
    }

    $sql = "SELECT * FROM laboratori";

    if($result= mysqli_query($link,$sql)){
        $all= mysqli_fetch_all($result);
        $laboratori= array();
        foreach ($all as $value) {
            array_push($laboratori,new laboratorio($value[1],$value[0],$value[2]));
        }
        echo json_encode($laboratori);
    }
    mysqli_close($link);
?>