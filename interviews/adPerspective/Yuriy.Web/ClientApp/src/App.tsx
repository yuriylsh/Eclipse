import * as React from 'react';
import { Route } from 'react-router';
import {Child1, Child2, Root} from './components/Root'

export default () => (
  <div>
    <Route exact={true} path='/' component={Root} />
    <Route path='/child1' component={Child1} />
    <Route path='/child2/:param1?' component={Child2} />
  </div>
);
