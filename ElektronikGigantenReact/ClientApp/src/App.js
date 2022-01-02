import React, { useEffect, useState, useContext } from "react";
import {Layout} from './components/Layout/Layout';
import {Route} from 'react-router';
import {Home} from './components/Layout/Home';
import Cart from './components/Cart/Cart';
import Login from './components/Layout/Login';
import Products from './components/products/Products';
import classes from './components/Layout/NavMenu.module.css';
import CartIcon from './components/Cart/CartIcon';
import CartContext from "./shop/CartContext";
import CartProvider from "./shop/CartProvider";

function App(props) {
  const [cartIsShown, SetCartIsShown] = useState(false);
  const [btnIsHighlighted, setBtnIsHighlighted] = useState(false);
  const cartCtx = useContext(CartContext);

  const showCartHandler = () => {
    SetCartIsShown(true);
  };

  const hideCartHandler = () => {
    SetCartIsShown(false);
  }


  const { items } = cartCtx;

  const numberOfCartItems = items.reduce((curNumber, item) => {
    return curNumber + item.amount;
  }, 0);

  const btnClasses = `${classes.button} ${btnIsHighlighted ? classes.bump : ''}`;

  useEffect(() => {
    if (items.length === 0) {
      return;
    }
    setBtnIsHighlighted(true);

    const timer = setTimeout(() => {
      setBtnIsHighlighted(false);
    }, 300);

    return () => {
      clearTimeout(timer);
    };
  }, [items]);
  return (
    <React.Fragment>  
    <CartProvider>
    {cartIsShown && <Cart onClose={hideCartHandler}/>}
    <button className={btnClasses} onClick={showCartHandler}>
    <span className={classes.icon}>
    <CartIcon />
    </span>
    <span>Your Cart</span>
    <span className={classes.badge}>{numberOfCartItems}</span>
    </button>
    <Layout> 
    <Route exact path='/' component={Home} />
    <Route path='/Products' component={Products} />
    <Route path='/Login' component={Login} />
    </Layout>
    </CartProvider>
    </React.Fragment>
  );
}

export default App;
