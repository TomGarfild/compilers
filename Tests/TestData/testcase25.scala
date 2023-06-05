class HelloWorld {
    // Print a hello message
    def hello: String {
        val name = "World"
        val message = "Hello, " + name + "!"
        print(message)
        message
    }

    // Print a goodbye message
    def goodbye: String {
        val name = "World"
        val message = "Goodbye, " + name + "!"
        print(message)
        message
    }
}
