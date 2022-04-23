<?php

class Pc {
	public $nome, $ip, $stato, $cod;
	public function __construct($nome, $ip, $stato, $cod){
		$this->nome=$nome;
		$this->ip=$ip;
		$this->stato=$stato;
		$this->cod=$cod;
	}
}

?>