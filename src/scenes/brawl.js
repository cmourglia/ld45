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
        this.createBounds();

        this.add.text(0, 0, `Brawl ${this.level}`, { fontFamily: 'Arial', fontSize: '100px' });
        this.enter = this.input.keyboard.addKey('ENTER');
    }

    update(_time, _dt) {
        if (this.enter.isDown) {
            this.scene.start('Copulate', { level: this.level + 1 });
        }
    }
}

export default Brawl;
