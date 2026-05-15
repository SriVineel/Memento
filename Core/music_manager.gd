extends Node

var music_player: AudioStreamPlayer

func _ready():
	# 1. Create the audio player that will run in the background
	music_player = AudioStreamPlayer.new()
	add_child(music_player)
	
	# (Change this to "Music" if you created a specific audio bus for it)
	music_player.bus = "Master" 

	# 2. Start your first track immediately
	play_track("res://Assets/Music/music1.mp3")

# 3. This is the missing function Godot was looking for!
func play_track(path: String):
	var new_stream = load(path)
	
	# Don't restart the song if it's already playing
	if music_player.stream == new_stream and music_player.playing:
		return 
		
	music_player.stream = new_stream
	music_player.play()

func stop_music():
	music_player.stop()
