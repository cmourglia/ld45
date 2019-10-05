import 'phaser'

import { Title } from './scenes/title.js';
import { SimpleScene } from './scenes/simple-scene';

new Phaser.Game({
	width: 3600,
	height: 3600,
	zoom: ".25",
	scene: [Title]
});
