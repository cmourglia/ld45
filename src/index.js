import 'phaser'

import { Title } from './scenes/title.js';
import { SimpleScene } from './scenes/copulate';

new Phaser.Game({
	backgroundColor: "#888888",
	width: 3600,
	height: 3600,
	zoom: ".25",
	scene: [Title],
	physics: {
		default: "matter",
		matter: {
		  gravity: { y:0.1 },
		  debug: true
		}
	  },
});
