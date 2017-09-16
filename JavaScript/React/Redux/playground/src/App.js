import './App.css';
import { connect } from 'react-redux';
import Root from './components/Root';

let dispatchOneCache;
const dispatchOne = (dispatch) => dispatchOneCache || (dispatchOneCache = function() {dispatch({type: "ONE"})});

let dispatchTwoCache;
const dispatchTwo = (dispatch) => dispatchTwoCache || (dispatchTwoCache = function(){dispatch({type: "TWO"})});

const App = connect(
  (state) => ({ ...state }),
  (dispatch) => {
    return {
      oneClicked: dispatchOne(dispatch),
      twoClicked: dispatchTwo(dispatch)
    };
  }
)(Root);

export default App;
