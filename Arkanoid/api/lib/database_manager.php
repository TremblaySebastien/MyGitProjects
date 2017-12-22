<?php
class DatabaseManager {
	private $connection;
	private $id; 
	private $schema;

	public function __construct($str = "") {

		$this->schema = $str;
		$servername = "localhost";
		$username = "root";
		$password = "";
		$this->connection = new mysqli($servername, $username, $password);
	}

	public function SqlExecute($query) {
		$result = $this->connection->query($query);
		$response = [];
		if($result !== FALSE && mysqli_num_rows($result) > 0) {
			while($row = mysqli_fetch_assoc($result)) {
				$response []= $row;
			}
		}
		return $response;
	}

	public function Sql($query) {
		$this->connection->query($query);
	}
}
?>
 