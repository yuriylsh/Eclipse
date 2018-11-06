import * as React from 'react'
import { RouteComponentProps } from 'react-router';
import { Link } from 'react-router-dom';

export const Root: React.SFC = () => <div className="root">This is root. Current environment is {process.env.NODE_ENV}. Go to <Link to="/child1">Child1</Link> or <Link to="child2/123">Child2</Link>.</div>
export const Child1: React.SFC = () => <div className="child1">This is Child1. Back to <Link to="/">root</Link>.</div>
export const Child2: React.SFC<RouteComponentProps<{param1: string}>> = (props) => <div className="child2">{`This is Child2 with param1 = ${props.match.params.param1}`}. Back to <Link to="/">root</Link>.</div>