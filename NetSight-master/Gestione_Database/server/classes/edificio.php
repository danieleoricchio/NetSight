<?php
    class edificio
    {
        public $nome,$cod,$indirizzo;
        public function __construct($cod,$nome,$indirizzo){
            $this -> nome = $nome;
            $this -> cod = $cod;
            $this -> indirizzo = $indirizzo;
        }
    }
?>