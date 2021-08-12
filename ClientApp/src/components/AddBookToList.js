import React, { useState, useEffect } from 'react'
import axios from 'axios'
import Book from './Book'
import Checkbox from './Checkbox'

export default function AddBookToList() {
  const [tome, SetTome] = useState({
    Title: '',
    Finished: null,
    Abandoned: null,
    FromLibrary: null,
    Owned: null,
    GivenAway: null,
  })
  const [tomeList, SetTomeList] = useState([])
  //const [checkboxValue, SetCheckboxValue] = useState([])
  const [checked, setChecked] = useState([])

  const checkboxOptions = [
    'Finished',
    'Abandoned',
    'FromLibrary',
    'Owned',
    'GivenAway',
  ]

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
        tome,
      })
      .then((resp) => {
        console.log({ resp })
        SetTomeList((oldList) => oldList.concat(resp.data))
        SetTome('')
      })
  }

  const handleTitle = (e) => {
    let titleText = e.target.value
    let updateTitle = { ...tome, Title: titleText }
    SetTome(updateTitle)
  }

  const handleCheckboxChange = (e) => {
    const { name } = e.target

    setChecked(e.target.checked)

    // setChecked((prevState) => ({
    //   checkboxes: {
    //     ...prevState.checkboxes,
    //     [name]: !prevState.checkboxes[name],
    //   },
    // }))
  }

  const createCheckbox = (option) => (
    <Checkbox
      label={option}
      // isSelected={this.state.checkboxes[option]}
      onCheckboxChange={handleCheckboxChange}
      key={option}
    />
  )

  const createCheckboxes = () => checkboxOptions.map(createCheckbox)

  return (
    <section className="container">
      <form className="form-center" onSubmit={addBookToList}>
        <div>
          <input
            className="book-input"
            type="text"
            placeholder="Did you buy another book?"
            value={tome.Title}
            onChange={handleTitle}
          />
        </div>
        <div>{createCheckboxes()}</div>
        <button type="submit" value="Submit" className="book-button">
          +
        </button>
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
