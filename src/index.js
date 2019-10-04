import 'phaser'

import { Title } from './scenes/title.js';
import { SimpleScene } from './scenes/simple-scene';

const gameConfig = {
	width: 680,
	height: 400,
	scene: [Title]
};

new Phaser.Game(gameConfig);
