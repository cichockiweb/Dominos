// See https://aka.ms/new-console-template for more information
using Dominos.Core.Dominos;
using Dominos.Core.OperationResults;
using Dominos.Infrastructure.Dominos;

Console.WriteLine(InfoMessages.ProvideInput);

var dominosInput = Console.ReadLine();

var dominosCollection = DominosCollection.Create(dominosInput);
if(!dominosCollection.IsSuccess){
    Console.WriteLine(dominosCollection.Error);
    return;
}

var chainMaker = new ChainMaker();
var dominoChain = chainMaker.Chain(dominosCollection.Value);

if(dominoChain.IsSuccess){
    Console.WriteLine(InfoMessages.Congratulations);
    Console.WriteLine(string.Join(',', dominoChain.Value.Select(x=>$"[{x.LeftSide}|{x.RightSide}]")));
}
Console.WriteLine(dominoChain.Error);
