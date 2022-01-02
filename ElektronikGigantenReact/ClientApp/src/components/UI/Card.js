import React, { useContext } from "react";

import classes from './Card.module.css';
import productPic from '../../assets/electronics2.jpg';
import CartContext from "../../shop/CartContext";



const Card = (props) => {

  const cartCtx = useContext(CartContext);

  const price = `${props.price.toFixed(2)}`;

  const addToCartHandler = amount => {
    cartCtx.addItem({
      id: props.id,
      name: props.name,
      amount: amount,
      price: props.price
    });
  };
    return (
      <React.Fragment>
        <li className={classes.card}>
          <h2 className={classes.title}>{props.name}</h2>
          <div className={classes.image}>
          <img className={classes.img} src={productPic} alt="Help me pls!"></img>
          </div>
          <h3 className={classes.price}>{price} kr.</h3>
          <h5 className={classes.quantity}>quantity: {props.quantity}</h5>
          <p className={classes.descrp}>{props.description}</p>
          <button className={classes.addToCart} onSubmit={addToCartHandler}>Add to Cart</button>
        </li>
        </React.Fragment>
      );
};

export default Card;
