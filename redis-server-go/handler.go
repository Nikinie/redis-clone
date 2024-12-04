package main

import "fmt"

var Handlers = map[string]func([]Value) Value{
	"PING": ping,
	"SET":  set,
	"GET":  get,
	"DEL":  del,
}

var store = map[string]Value{}

func ping(args []Value) Value {
	return Value{typ: "string", str: "PONG"}
}

func set(args []Value) Value {
	errorValue, ok := argumentLengthError(2, len(args), "set")
	if !ok {
		return errorValue
	}

	key := args[0].bulk
	value := args[1]
	store[key] = value
	return Value{typ: "string", str: "OK"}
}

func get(args []Value) Value {
	errorValue, ok := argumentLengthError(1, len(args), "get")
	if !ok {
		return errorValue
	}

	key := args[0].bulk
	value, ok := store[key]

	if !ok {
		return Value{typ: "null", str: "(nil)"}
	}

	return value
}

func del(args []Value) Value {
	errorValue, ok := argumentLengthError(1, len(args), "del")
	if !ok {
		return errorValue
	}

	key := args[0].str
	delete(store, key)

	return Value{typ: "integer", num: 1}
}

func argumentLengthError(expected int, given int, command string) (Value, bool) {
	errorValue := Value{typ: "string", str: fmt.Sprintf("ERR wrong number of arguments for command %s, expected %d, got %d\n", command, expected, given)}
	ok := expected == given
	return errorValue, ok
}
