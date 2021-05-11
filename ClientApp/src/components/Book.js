import React, { Component } from 'react'

class Book extends Component {
  render() {
    return (
      <div>
        <p>{this.props.Name}</p>
      </div>
    )
  }
}

export default Book
