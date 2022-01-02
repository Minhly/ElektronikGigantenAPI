import React, { useEffect, useState } from "react";

import classes from "./Products.module.css";
import Card from "../UI/Card";

const Products = () => {
  const [error, setError] = useState(null);
  const [isLoaded, setIsLoaded] = useState(false);
  const [items, setItems] = useState([]);
  const [filteredItems, setFilteredItems] = useState([])
  const [search, setSearch] = useState("");

  const fetchItems = async () => {
    try {
      const response = await fetch("http://localhost:13978/api/Products");
      const loadedItems = await response.json();
      setItems(loadedItems);
      setFilteredItems(loadedItems);
    } catch (error) {
      console.log(error.message);
    }
    setIsLoaded(true);
  };

  useEffect(() => {
    fetchItems();
  }, []);

  const onChangeHandler = (event) => {
    const { value: searchTerm } = event.target;
    console.log(searchTerm);
    setSearch(searchTerm);

    let newFilteredItems = items.filter((item) =>
      item.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
    console.log(newFilteredItems);
    if (searchTerm === "") {
      setFilteredItems(items);
    } else {
      setFilteredItems(newFilteredItems);
    }
    
  };

  if (error) {
    return <div>Error: {error.message}</div>;
  } else if (!isLoaded) {
    return <div>Loading...</div>;
  } else {
    return (
      <div>
        <input
          placeholder="Search Products"
          value={search}
          onChange={onChangeHandler}
        />
        <ul className={classes.productlist}>
          {filteredItems.map((item, index) => (
            <div key={index}>
              <Card
                key={item.id}
                name={item.name}
                price={item.price}
                quantity={item.quantity}
                description={item.description}
              />
            </div>
          ))}
        </ul>
      </div>
    );
  }
};

export default Products;
