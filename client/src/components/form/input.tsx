type Props = {
  name: string
  password?: boolean
}

function Input({ name, password }: Props) {
  return (
    <div className="flex flex-col gap-2">
      <label htmlFor={name} className="capitalize  font-medium">
        {name}
      </label>
      <input
        type={password ? 'password' : 'text'}
        name={name}
        className="border border-border rounded focus:outline-1 focus:outline-offset-1 px-2 py-1 opacity-80 focus:opacity-100 transition-all"
      />
    </div>
  )
}

export default Input
