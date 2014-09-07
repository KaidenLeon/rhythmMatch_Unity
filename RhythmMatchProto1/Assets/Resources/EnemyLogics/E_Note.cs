
public class E_Note {

	float endTime = 9999.0f;
	NoteType noteType = NoteType.NOTE_TYPE_NONE;

	public E_Note( float endTime, NoteType noteType )
	{
		this.endTime = endTime;
		this.noteType = noteType;
	}

	public float GetEndTime()
	{
		return endTime;
	}
	public NoteType GetNoteType()
	{
		return noteType;
	}
}
