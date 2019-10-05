import Blob from './blob';

const dot = (v, w) => v.x * w.x + v.y * w.y;
const length = (v) => Math.sqrt(dot(v, v));
const normalize = (v) => {
    const l = 1 / length(v);
    return {
        x: v.x * l,
        y: v.y * l,
    };
};

class Agent extends Blob {
    constructor(scene, player) {
        super(scene);
        this.player = player;
    }

    update(_time, _dt) {
        const targetPosition = this.player.getPosition();
        const currentPosition = this.getPosition();

        let deltaPosition = {
            x: targetPosition.x - currentPosition.x,
            y: targetPosition.y - currentPosition.y,
        };

        deltaPosition = normalize(deltaPosition);
        this.setVelocity(deltaPosition.x * 5, deltaPosition.y * 5);
    }
}

export default Agent;
