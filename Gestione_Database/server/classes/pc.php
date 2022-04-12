<?php

class Pc {
	public $nome, $ip, $stato;
	public function __construct($nome, $ip, $stato){
		$this->nome=$nome;
		$this->ip=$ip;
		$this->stato=$stato;
	}
}

?>