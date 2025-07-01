import { useState } from 'react'
import { Link, createFileRoute, useNavigate } from '@tanstack/react-router'
import Form from '@/components/form'
import Input from '@/components/form/input'

export const Route = createFileRoute('/register')({
  component: RouteComponent,
})

function RouteComponent() {
  const errorsDefault = {
    username: false,
    email: false,
    password: false,
    repassword: false,
  }
  const [errors, setErrors] = useState({
    username: false,
    email: false,
    password: false,
    repassword: false,
  })
  const [error, setError] = useState<string>('')
  const nav = useNavigate()
  const submit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()
    const formdata = new FormData(e.currentTarget)
    const username = formdata.get('username')
    const email = formdata.get('email')
    const password = formdata.get('password')
    const repassword = formdata.get('retype password')

    if (password !== repassword) {
      setErrors({ ...errorsDefault, repassword: true })
      setError('Passwords do not match')
      return
    } else if (!email?.toString().includes('@')) {
      setErrors({ ...errorsDefault, email: true })
      setError('Email is not valid')
      return
    } else if (password!.toString().length < 6) {
      setErrors({
        ...errorsDefault,
        password: true,
      })
      setError('Password must have at least 6 characters')
      return
    } else if (username!.toString().length < 3) {
      setErrors({
        ...errorsDefault,
        username: true,
      })
      setError('Username must have at least 3 characters')
      return
    }

    const res = await fetch('http://localhost:5000/api/register', {
      body: JSON.stringify({
        username,
        email,
        password,
      }),
      method: 'POST',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
    })

    const json = await res.json()
    if (!json.error) {
      nav({
        to: '/user',
      })
      return
    }
    if (json.duplicateEmail) {
      setErrors({ ...errorsDefault, email: true })
      setError('Email already exists')
      return
    } else if (json.duplicateUsername) {
      setErrors({ ...errorsDefault, username: true })
      setError('Username already exists')
      return
    }
  }
  return (
    <Form submit={submit}>
      <h1 className="text-4xl text-primary absolute top-48 uppercase font-bold">
        Create Account
      </h1>
      <Input name="username" error={errors.username} />
      <Input name="email" error={errors.email} />
      <Input name="password" error={errors.password} password />
      <Input name="retype password" error={errors.repassword} password />
      {error && <h1 className="text-red-500 absolute bottom-96">{error}</h1>}
      <div className="flex flex-col gap-2 items-center">
        <button className="mt-8 font-medium cursor-pointer px-3 py-1 rounded bg-primary hover:opacity-80 transition-all">
          Create Account
        </button>
        <span className="opacity-80">or</span>
        <Link
          to="/login"
          className="opacity-80 hover:opacity-100 transition-all"
        >
          Log In
        </Link>
      </div>
    </Form>
  )
}
