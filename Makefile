e%: e%.ml
	ocamlopt -o $@ $^

clean:
	-rm -f *.~undo-tree~ **.cmi *.cmx *.o
