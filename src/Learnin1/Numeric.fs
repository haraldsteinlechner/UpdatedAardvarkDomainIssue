namespace Numeric

open Aardvark.Base.Incremental

[<DomainType>]
type State ={
    value:float
}
type Msg = 
|Increment
|Decrement

[<AutoOpen>]
module Numeric = 
    let update m msg=             
        match msg with 
        | Increment -> {m with value = m.value + 0.1}
        | Decrement -> {m with value = m.value - 0.1}
