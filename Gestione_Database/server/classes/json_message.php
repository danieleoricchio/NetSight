<?php
    class JsonMessage
    {
        public $number_code, $message, $result;
        public function __construct(Int $number_code, string $message, bool $result) {
            $this->number_code = $number_code;
            $this->message = $message;
            $this->result = $result;
        }
    }
    
?>