
# Breweries Management System

API to help breweries, and wholesaler manage their stock, sales, and order properly.


## API Reference

#### Get all beers by brewery

```http
  GET /api/breweries/{breweryId}/beers
```

| Parameter | Type     | Description                |
| :-------- | :------- | :------------------------- |
| `breweryId` | `int` | **Required**. Get all beers for a brewery |

#### Adding a beer for a brewery

```http
  POST /api/breweries/
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `beer`      | `CreateBeerDto` | **Required**. Adding a beer to  brewery |

#### Deleting a beer by the brewery (owner)

```http
  DELETE /api/breweries/{breweryId}/beer/{beerId}
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `breweryId`  `beerId`      | `int` | **Required**. Deleting a beer |

#### Adding a sale by the wholesaler

```http
  POST /api/wholesaler
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `beerStockDto` | `BeerStockDto` | **Required**. Adding a sale (beerStock) to a wholesaler |

#### Updatting a remaining stock by the wholesaler(owner)

```http
  PUT /api/wholesaler
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `beerStockDto` | `BeerStockDto` | **Required**. updatting a remaining stock |

#### Adding an Order

```http
  PUT /api/orders
```

| Parameter | Type     | Description                       |
| :-------- | :------- | :-------------------------------- |
| `order` | `Order` | **Required**. Adding new order |