
public enum NoteType
{
	NOTE_TYPE_NONE,
	NOTE_TYPE_LEFT,
	NOTE_TYPE_RIGHT,
	NOTE_TYPE_MAX
}

public class Note {

	float endTime = 9999.0f;
	NoteType noteType = NoteType.NOTE_TYPE_NONE;

	public Note( float endTime, NoteType noteType )
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
