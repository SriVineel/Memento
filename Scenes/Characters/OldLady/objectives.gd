extends PanelContainer

@onready var objectives_list = $VBoxContainer/ObjectivesList

var objectives = [
	"Prepare the house for your daughter's visit",
	"Interact with the teddy bear on the shelf",
	"Clean the table(Get Cloth from second room in top floor)",
	"Decorate with the flower pot(Get it from rooftop)",
	"Get your Medicine (Ground Floor)",
    "Answer the door"
]

var current_index = 1
var completed = []

func _ready():
	visible = true
	position = Vector2(20, 20)
	modulate.a = 0.0
	# Restore state from Global
	current_index = Global.current_objective_index
	completed = Global.completed_objectives
	_refresh_list()
	var tween = create_tween()
	tween.tween_property(self, "modulate:a", 1.0, 2.0)

func _process(delta):
	visible = true
	position = Vector2(20, 20)
	
	# NEW: Constantly check if a different script updated the Global index!
	if current_index != Global.current_objective_index:
		current_index = Global.current_objective_index
		completed = Global.completed_objectives
		_refresh_list()

func _refresh_list():
	for child in objectives_list.get_children():
		child.queue_free()

	# Main objective always shows at top
	var main_label = Label.new()
	main_label.text = "🎯 " + objectives[0]
	main_label.modulate = Color(1, 0.85, 0.3)  # gold
	main_label.autowrap_mode = TextServer.AUTOWRAP_WORD_SMART
	objectives_list.add_child(main_label)

	# Show only current active objective below
	if current_index < objectives.size():
		var sep = Label.new()
		sep.text = "──────────"
		sep.modulate = Color(0.5, 0.5, 0.5)
		objectives_list.add_child(sep)

		var label = Label.new()
		label.text = "• " + objectives[current_index]
		label.modulate = Color(1, 1, 1)
		label.autowrap_mode = TextServer.AUTOWRAP_WORD_SMART
		objectives_list.add_child(label)

func complete_objective(obj_text: String):
	if obj_text in objectives and obj_text not in completed:
		completed.append(obj_text)
		Global.completed_objectives = completed
		current_index += 1
		Global.current_objective_index = current_index
		_refresh_list()
