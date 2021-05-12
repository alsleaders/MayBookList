// import React, { useState, useEffect } from 'react'
import React, { Component } from 'react'
//import axios from 'axios'
import Book from './Book'

// const [books, setBooks] = useState([])

//const API_URL = '/api/Books'

// useEffect(() => {
//   axios.get('/api/Books').then(resp => {
//     console.log(resp.data)
//     setBooks(resp.data)
//   })
// }, [])

class Container extends Component {
  state = {
    books: [],
  }

  componentDidMount() {
    // axios.get('/api/Books').then((resp) => {
    //   console.log(resp.data)
    //   this.setState({
    //     books: resp.data,
    //   })
    // })
    fetch('/api/Books')
      .then((response) => {
        return response.json()
      })
      .then((json) => {
        console.log(json)
        this.setState({ books: json })
      })
  }

  // componentDidMount() {
  //   fetch(`${API_URL}`).then((resp) => {
  //     console.log(resp.json)
  //     this.setState({ books: resp.json })
  //   })
  //   // .then((items) => {
  //   //   console.log(items)
  //   //   this.setState({ books: items })
  //   // })
  // }

  render() {
    return (
      <>
        <div className="container">
          <section>
            {this.state.books.map((book) => {
              return <Book key={book.id} value={book.title} />
            })}
          </section>
        </div>
      </>
    )
  }
}

export default Container
