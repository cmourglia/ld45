import Phaser from 'phaser';

import Title from './scenes/title';

const game = new Phaser.Game({
    backgroundColor: '#888888',
    width: 900,
    height: 900,
    scene: [Title],
    type: Phaser.WEBGL,
    physics: {
        default: 'matter',
        matter: {
            gravity: { y: 0 },
            debug: true,
        },
    },
});
