import Base from './base';

class Brawl extends Base {
    constructor() {
        super();
        this.text = null;
    }

    init(props) {
        this.level = props.level;
    }

    create() {
        super.create();

        this.add.text(0, 0, `Brawl ${this.level}`, { fontFamily: 'Arial', fontSize: '100px' });
        this.enter = this.input.keyboard.addKey('ENTER');
    }

    update(time, dt) {
        if (this.enter.isDown) {
            this.scene.start('Copulate', { level: this.level + 1 });
        }

        super.update(time, dt);
    }
}

export default Brawl;
