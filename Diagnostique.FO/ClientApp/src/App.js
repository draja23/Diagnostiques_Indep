﻿import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import Home from './components/Home';
import Counter from './components/Counter';
import FetchData from './components/FetchData';
import { GetSource } from './components/GetSourceComponent/GetSource';
import { GetItems } from './components/GetItemsComponent/GetItems';

export default () => (
  <Layout>
        <Route exact path='/' component={Home} />
        <Route path='/getSource' component={GetSource} />
        <Route path='/getItems' component={GetItems} />   
  </Layout>
);
