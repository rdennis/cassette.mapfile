class Tester {
    constructor(public message = 'no message') { }

    test() {
        alert('this is a test:\n' + this.message)
    }
}