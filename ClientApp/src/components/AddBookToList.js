import React, { useState, useEffect } from 'react'
import axios from 'axios'
import Book from './Book'

export default function AddBookToList() {
  const [tome, SetTome] = useState('')
  const [tomeList, SetTomeList] = useState([])

  useEffect(() => {
    axios.get(`api/Books`).then((resp) => {
      console.log(resp.data)
      SetTomeList(resp.data)
    })
  }, [])

  const addBookToList = (e) => {
    e.preventDefault()
    console.log({ tome })
    axios
      .post(`api/Books`, {
        name: tome,
      })
      .then((resp) => {
        console.log({ resp })
        SetTomeList((oldList) => oldList.concat(resp.data))
        SetTome('')
      })
  }

  return (
    <section className="container">
      <form className="form-center" onSubmit={addBookToList}>
        <div>
          <input
            className="book-input"
            type="text"
            placeholder="Did you buy another book?"
            value={tome.name}
            onChange={(e) => {
              SetTome(e.target.value)
            }}
          />
        </div>
        <button className="book-button">+</button>
      </form>
      {/* <ul>
        {tomeList.map((item) => {
          return (
            <Book
              key={item.id}
              id={item.id}
              book={item.name}
              isCompleted={item.Finished}
            />
          )
        })}
      </ul> */}
    </section>
  )
}
