extends Node

signal dialogue_finished

var dialogue_lines: Array = [
	{ "speaker": "Brother", "text": "Eleanor! Sit with me. We have all the time in the world." },
	{ "speaker": "Eleanor", "text": "Do you think we'll always be like this?" },
	{ "speaker": "Brother", "text": "Like what?" },
	{ "speaker": "Eleanor", "text": "Happy. Just... happy." },
]

var current_line: int = 0
var is_active: bool = false
var is_typing: bool = false
var full_text: String = ""
var tween: Tween

var panel
var speaker_label
var text_label
var continue_hint

func _ready():
	panel         = get_node("/root/Memory0/CanvasLayer/DialougePanel")
	speaker_label = get_node("/root/Memory0/CanvasLayer/DialougePanel/VBoxContainer/SpeakerLabel")
	text_label    = get_node("/root/Memory0/CanvasLayer/DialougePanel/VBoxContainer/TextLabel")
	continue_hint = get_node("/root/Memory0/CanvasLayer/DialougePanel/VBoxContainer/ContinueHint")
	panel.visible = false

func start_dialogue():
	current_line = 0
	is_active = true
	panel.visible = true
	_show_line(current_line)

func _show_line(index: int):
	if index >= dialogue_lines.size():
		_end_dialogue()
		return
	var line = dialogue_lines[index]
	speaker_label.text = line["speaker"]
	text_label.text = ""
	full_text = line["text"]
	continue_hint.visible = false
	is_typing = true
	tween = create_tween()
	for i in range(full_text.length()):
		tween.tween_callback(_add_character.bind(i)).set_delay(0.03 )
	tween.tween_callback(_on_typing_finished)

func _add_character(index: int):
	text_label.text = full_text.substr(0, index + 1)

func _on_typing_finished():
	is_typing = false
	continue_hint.visible = true

func _input(event):
	if not is_active:
		return
	if event is InputEventKey and event.pressed:
		if is_typing:
			if tween:
				tween.kill()
			text_label.text = full_text
			is_typing = false
			continue_hint.visible = true
		else:
			current_line += 1
			_show_line(current_line)

func _end_dialogue():
	is_active = false
	panel.visible = false
	emit_signal("dialogue_finished")
