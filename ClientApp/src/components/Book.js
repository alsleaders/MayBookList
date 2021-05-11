import React, { Component } from 'react'

class Book extends Component {
  render() {
    return (
      <div>
        <p>{this.props.value}</p>
      </div>
    )
  }
}

export default Book
