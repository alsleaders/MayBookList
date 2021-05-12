//import React from 'react'
import './custom.scss'
import Container from './components/Container'
import Greetings from './components/Greetings'
import AddBookToList from './components/AddBookToList'

import React from 'react'
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom'

export default function App() {
  return (
    <Router>
      <div>
        <nav>
          <ul className="navbar">
            <li>
              <Link to="/">Home</Link>
            </li>
            <li>
              <Link to="/about">About</Link>
            </li>
            <li>
              <Link to="/addBook">Add Book</Link>
            </li>
            <li>
              <Link to="/allBooks">All Books</Link>
            </li>
          </ul>
        </nav>

        {/* A <Switch> looks through its children <Route>s and
            renders the first one that matches the current URL. */}
        <Switch>
          <Route path="/about">
            <About />
          </Route>
          <Route path="/addBook">
            <AddBookToList />
          </Route>
          <Route path="/allBooks">
            <Container />
          </Route>
          <Route path="/">
            <Home />
          </Route>
        </Switch>
      </div>
    </Router>
  )
}

function Home() {
  return (
    <h2>
      <Greetings />
    </h2>
  )
}

function About() {
  return <h2>About</h2>
}

function AddBook() {
  return (
    <h2>
      <AddBookToList />
    </h2>
  )
}

function AllBooks() {
  return (
    <h2>
      <Container />
    </h2>
  )
}

// export default function App() {
//   return (
//     <>
//       <Greetings />
//       <AddBookToList />
//     </>
//   )
// }
