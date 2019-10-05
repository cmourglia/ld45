class Vector {
    static add(v1, v2) {
        return {
            x: v1.x + v2.x,
            y: v1.y + v2.y,
        };
    }

    static sub(v1, v2) {
        return {
            x: v1.x - v2.x,
            y: v1.y - v2.y,
        };
    }

    static mul(v, x) {
        return {
            x: v.x * x,
            y: v.y * x,
        };
    }

    static dot(v1, v2) {
        return v1.x * v2.x + v1.y * v2.y;
    }

    static length(v) {
        return Math.sqrt(Vector.dot(v, v));
    }

    static invLength(v) {
        return 1.0 / Math.sqrt(Vector.dot(v, v));
    }

    static normalize(v) {
        return Vector.mul(v, Vector.invLength(v));
    }

    static cross(v1, v2) {
        return v1.x * v2.y - v1.y * v2.x;
    }

    static distance(v1, v2) {
        return Vector.length(Vector.sub(v2, v1));
    }

    static direction(v1, v2) {
        return Vector.normalize(Vector.sub(v2, v1));
    }
}

export default Vector;
