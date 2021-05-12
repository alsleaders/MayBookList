import React, { useState } from 'react'
import axios from 'axios'

export default function BookItem(props) {
  const [IsFinished, setIsFinished] = useState(props.isFinished)
  const [errorMessage, setErrorMessage] = useState('')

  const toggleCompletion = () => {
    setIsFinished((oldIsFinished) => !oldIsFinished)
    axios
      .put(`api/Books/${props.id}`, {
        completed: IsFinished,
        name: props.book,
      })
      .then((resp) => {
        console.log({ resp })
        console.log({ IsFinished })
        if (resp.status !== 200) {
          setErrorMessage('WARNING, change not saved')
        }
      })
  }

  return (
    <>
      <section>
        <div className="each-book">
          <li className={IsFinished ? 'completed' : ''}>
            <h4 className="item-text">{props.book}</h4>
            <button onClick={toggleCompletion}>
              {IsFinished ? 'undo' : 'complete'}
            </button>
            {errorMessage && <p className="errorMessage">{errorMessage}</p>}
          </li>
        </div>
      </section>
    </>
  )
}
