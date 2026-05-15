shader_type canvas_item;

uniform float vignette_strength : hint_range(0.0, 1.0) = 0.5;
uniform float blur_strength : hint_range(0.0, 1.0) = 0.3;

void fragment() {
	vec2 uv = UV;
	
	// Vignette
	vec2 center = uv - 0.5;
	float vignette = 1.0 - dot(center, center) * 2.5 * vignette_strength;
	
	// Soft edge blur simulation
	float edge = smoothstep(0.0, blur_strength, min(uv.x, min(uv.y, min(1.0 - uv.x, 1.0 - uv.y))));
	
	// Black and white tint at edges
	vec4 col = texture(TEXTURE, uv);
	float gray = dot(col.rgb, vec3(0.299, 0.587, 0.114));
	vec3 bw = vec3(gray);
	col.rgb = mix(bw, col.rgb, edge);
	
	COLOR = vec4(col.rgb * vignette, col.a);
}
