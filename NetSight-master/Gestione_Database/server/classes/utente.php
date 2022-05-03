<?php
    class utente
    {
        public $nome,$cod,$cognome, $dataNascita, $email;
        public function __construct($cod,$nome,$cognome, $dataNascita, $email){
            $this -> nome = $nome;
            $this -> cod = $cod;
            $this -> cognome = $cognome;
            $this -> dataNascita = $dataNascita;
            $this -> email = $email;
        }
    }
?>