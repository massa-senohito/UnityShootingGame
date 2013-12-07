open util/ordering[Time]
sig Time{}
enum Lock{L,U}

one sig Player{
  
}

one sig MarkManager {

}
abstract sig  Enemy {}
abstract sig  Mark {lock:Enemy->Lock->Time}{
  all t:Time| one lock.t 
}
pred lockone(l:Lock)
fact {
  all l:
}
one sig Mark1 extends Mark{}
one sig Mark3 extends Mark{}
one sig Mark2 extends Mark{}
run {}
