extends Node2D


func _ready():
	BgMusic.Play("Song1")
	
	var book = load("res://Main/Scenes/Items/Books.tscn").instantiate()
	book.position = Vector2(200, -175)
	add_child(book)
	
	var bugs = load("res://Main/Scenes/Items/Tarakonas.tscn").instantiate()
	bugs.position = Vector2(-1410, -80)
	add_child(bugs)
	
	var player = load(Game.selected_character).instantiate()
	add_child(player)
	
	player.position = Vector2(Game.cordx, Game.cordy)
	if Game.dialogue == "GameStart":
		DialogueManager.show_example_dialogue_balloon(load("res://Dialogai/gamestart.dialogue"))
		
		Game.dialogue = "NULL"
	if Game.books == "1" or Game.books == "2" :
		book.queue_free()
	if Game.bug == "1" :
		bugs.queue_free()

