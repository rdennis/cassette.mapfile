var Tester = (function () {
    function Tester(message) {
        if (message === void 0) { message = 'no message'; }
        this.message = message;
    }
    Tester.prototype.test = function () {
        alert('this is a test:\n' + this.message);
    };
    return Tester;
})();
//# sourceMappingURL=tester.js.map