import React, { Component } from 'react'
import Book from './Book'

class Container extends Component {
  state = {
    books: [],
  }

  componentDidMount() {
    fetch('/api/Books')
      .then((response) => {
        return response.json()
      })
      .then((json) => {
        console.log(json)
        this.setState({ books: json })
      })
  }

  render() {
    return (
      <>
        <div className="container">
          <section className="book-section">
            {this.state.books.map((book) => {
              return (
                <Book
                  key={book.id}
                  id={book.id}
                  book={book.title}
                  isFinished={book.Finished}
                  value={book.title}
                />
              )
            })}
          </section>
        </div>
      </>
    )
  }
}

export default Container
