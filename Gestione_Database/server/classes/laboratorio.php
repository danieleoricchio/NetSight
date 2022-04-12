<?php
class laboratorio
    {
        public $nome,$cod,$codEdificio,$listaPc;
        public function __construct($cod,$nome,$codEdificio){
            $this -> nome = $nome;
            $this -> cod = $cod;
            $this -> codEdificio = $codEdificio;
            $this -> listaPc = array();
        }
    }
?>