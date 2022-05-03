<?php
    class JsonMessage
    {
        public $number_code, $message, $status, $result;
        public function __construct(Int $number_code, string $message, bool $status, $result) {
            $this->number_code = $number_code;
            $this->message = $message;
            $this->result = $result;
            $this->status = $status;
        }
    }
    
?>