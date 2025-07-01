import { useState } from 'react'
import { Link, createFileRoute, useNavigate } from '@tanstack/react-router'
import Form from '@/components/form'
import Input from '@/components/form/input'

export const Route = createFileRoute('/login')({
  component: RouteComponent,
})

function RouteComponent() {
  const [error, setError] = useState('')
  const nav = useNavigate()
  const submit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault()

    const formdata = new FormData(e.currentTarget)
    const email = formdata.get('email')
    const password = formdata.get('password')

    const res = await fetch('http://localhost:5000/api/login', {
      method: 'POST',
      credentials: 'include',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({
        email,
        password,
        username: "doesn't matter",
      }),
    })
    const json = await res.json()

    if (!json.error) {
      nav({
        to: '/user',
      })
      return
    }

    setError('Invalid email or password')
  }

  return (
    <Form submit={submit}>
      <h1 className="text-3xl font-bold uppercase absolute top-64 text-primary">
        Log In
      </h1>
      <Input name="email" error={error.length > 0} />
      <Input name="password" error={error.length > 0} password />
      <h1 className="text-red-500 absolute bottom-[465px]">{error}</h1>
      <div className="mt-8 flex flex-col gap-2 items-center">
        <button className="bg-primary px-3 py-1 rounded font-medium cursor-pointer hover:opacity-90 transition-all">
          Log In
        </button>
        <span className="opacity-80">or</span>
        <Link
          to="/register"
          className="opacity-80 hover:opacity-100 transition-all"
        >
          Create Account
        </Link>
      </div>
    </Form>
  )
}
