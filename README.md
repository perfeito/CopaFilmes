# CopaFilmes

Projeto de exemplo feito com Xamarin.Forms e Azure Function.


## API

A [API](https://moviecupfunction.azurewebsites.net):

método de teste GET:

### [GET]
```
https://moviecupfunction.azurewebsites.net/api/MovieList
```
* Retorna uma lista com todos os filmes:

### [POST]
```
https://moviecupfunction.azurewebsites.net/api/Winner
```
* Deve ser enviado o json com a lista de 8 filmes escolhidos e será retornado um json do os 2 filmes ganhadores:


### Teste da API
* A Azure Function possui um projeto de [Teste](https://github.com/perfeito/CopaFilmes/blob/dev/MovieCup/MovieCup.Test/UnitTest.cs) desenvolvido em xUnit


## APP

O App foi desenvolvido em Xamarin.Forms para as plataformas iOS e Android.


## Autor
* **Faberson Perfeito**
